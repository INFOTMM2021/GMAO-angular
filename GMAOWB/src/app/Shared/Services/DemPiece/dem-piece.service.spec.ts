import { TestBed } from '@angular/core/testing';

import { DemPieceService } from './dem-piece.service';

describe('DemPieceService', () => {
  let service: DemPieceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DemPieceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
