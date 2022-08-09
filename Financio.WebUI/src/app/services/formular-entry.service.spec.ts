import { TestBed } from '@angular/core/testing';

import { FormularEntryService } from './formular-entry.service';

describe('FormularEntryService', () => {
  let service: FormularEntryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormularEntryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
