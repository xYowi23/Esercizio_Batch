using System;
using System.Collections.Generic;

namespace Batch.Models;

public partial class CodiceFiscale
{
    
    public int IdCodFis { get; set; }

    public string CodiceFis { get; set; } = null!;

    public DateOnly DataEmis { get; set; }

    public DateOnly DataScad { get; set; }

    public int? PersonaRiff { get; set; }

    public virtual Persona? PersonaRiffNavigation { get; set; }
}
