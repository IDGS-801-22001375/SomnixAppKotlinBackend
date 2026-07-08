using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DTO.Telemetria
{
    public class TelemetriaDto
    {
        public float p { get; set; }  // Pitch
        public float r { get; set; }  // Roll
        public int n { get; set; }    // NivelAlerta
        public int t { get; set; }    // ModoTest
        public int v { get; set; }    // EnViaje
        public int hc { get; set; }   // HttpCode
        public long tr { get; set; }  // TiempoReaccionMs
    }
}
