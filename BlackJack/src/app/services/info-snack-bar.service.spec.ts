import { TestBed } from '@angular/core/testing';

import { InfoSnackBarService } from './info-snack-bar.service';

describe('InfoSnackBarService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InfoSnackBarService = TestBed.get(InfoSnackBarService);
    expect(service).toBeTruthy();
  });
});
