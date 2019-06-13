import { TestBed } from '@angular/core/testing';

import { DepartureTableService } from './departure-table.service';

describe('DepartureTableService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DepartureTableService = TestBed.get(DepartureTableService);
    expect(service).toBeTruthy();
  });
});
