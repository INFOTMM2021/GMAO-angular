

export interface ICentreCout {
    CodeCC?: string;
    Designation?: string;
    CodeAnt?: string;
    Seq?: number;
  }


export class CentreCout implements ICentreCout{
    constructor(
      public CodeCC?: string,
      public Designation?: string,
      public CodeAnt?: string,
      public Seq?: number,
    ) {}

    public equals(obj: CentreCout) : boolean { 
      return this.Seq === obj.Seq;
  } 
 }

