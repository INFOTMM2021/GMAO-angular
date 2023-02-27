import { TestBed } from '@angular/core/testing';

import { MvtArtService } from './mvt-art.service';

describe('MvtArtService', () => {
  let service: MvtArtService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MvtArtService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
