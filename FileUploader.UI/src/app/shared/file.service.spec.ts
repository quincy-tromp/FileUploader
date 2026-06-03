import { TestBed } from '@angular/core/testing';

import { FileService } from '../shared/file.service';

describe('File', () => {
  let service: FileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
