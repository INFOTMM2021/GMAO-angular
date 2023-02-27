import { TestBed } from '@angular/core/testing';

import { EquipementCompteurService } from './equipement-compteur.service';

describe('EquipementCompteurService', () => {
  let service: EquipementCompteurService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EquipementCompteurService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
