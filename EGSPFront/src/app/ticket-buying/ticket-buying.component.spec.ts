import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketBuyingComponent } from './ticket-buying.component';

describe('TicketBuyingComponent', () => {
  let component: TicketBuyingComponent;
  let fixture: ComponentFixture<TicketBuyingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketBuyingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketBuyingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
