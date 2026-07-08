using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DTO.Telemetria
{
    public class TelemetriaFirebase
    {
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public int NivelAlerta { get; set; }
        public bool ModoTest { get; set; }
        public bool EnViaje { get; set; }
        public int LastHttpCode { get; set; }
        public string FechaRegistro { get; set; } = string.Empty;
        public long TiempoReaccionMs { get; set; }
    }
}
