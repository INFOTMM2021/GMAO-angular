import { TestBed } from '@angular/core/testing';

import { FournisseurPieceService } from './fournisseur-piece.service';

describe('FournisseurPieceService', () => {
  let service: FournisseurPieceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FournisseurPieceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
