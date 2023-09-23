import { TestBed } from '@angular/core/testing';

import { VersionConverterService } from './version-converter.service';

describe('VersionConverterService', () => {
  let service: VersionConverterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VersionConverterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getAllowedVersions', () => {
    it('Should return number list if succesful', () => {
      service.getAllStaticChangesFromVersions();
    });
  });
});
