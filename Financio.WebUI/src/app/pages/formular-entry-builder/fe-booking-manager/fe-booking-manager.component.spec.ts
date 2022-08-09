import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeBookingManagerComponent } from './fe-booking-manager.component';

describe('FeBookingManagerComponent', () => {
  let component: FeBookingManagerComponent;
  let fixture: ComponentFixture<FeBookingManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeBookingManagerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FeBookingManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
