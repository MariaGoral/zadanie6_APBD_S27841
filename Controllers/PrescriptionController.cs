using Microsoft.AspNetCore.Mvc;
using PrescriptionAPI.Models;
using PrescriptionAPI.Services;

namespace PrescriptionAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpPost("prescription")]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequest request)
        {
            try
            {
                var prescription = new Prescription
                {
                    Date = request.Date,
                    DueDate = request.DueDate
                };

                var mappedMeds = request.Medicaments
                    .Select(m => (m.IdMedicament, m.Dose, m.Description))
                    .ToList();

                await _service.AddPrescriptionAsync(prescription, request.Patient, request.Doctor, mappedMeds);
                return Ok("Prescription added");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _service.GetPatientDataAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }
    }

    public class PrescriptionRequest
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<MedicamentData> Medicaments { get; set; }
    }

    public class MedicamentData
    {
        public int IdMedicament { get; set; }
        public int Dose { get; set; }
        public string Description { get; set; }
    }
}