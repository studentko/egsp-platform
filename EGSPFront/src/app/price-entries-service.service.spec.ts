import { TestBed } from '@angular/core/testing';

import { PriceEntriesServiceService } from './price-entries-service.service';

describe('PriceEntriesServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PriceEntriesServiceService = TestBed.get(PriceEntriesServiceService);
    expect(service).toBeTruthy();
  });
});
