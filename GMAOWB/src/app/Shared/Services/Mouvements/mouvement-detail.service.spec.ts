import { TestBed } from '@angular/core/testing';

import { MouvementDetailService } from './mouvement-detail.service';

describe('MouvementDetailService', () => {
  let service: MouvementDetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MouvementDetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
