import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormularEntryBuilderComponent } from './formular-entry-builder.component';

describe('FormularEntryBuilderComponent', () => {
  let component: FormularEntryBuilderComponent;
  let fixture: ComponentFixture<FormularEntryBuilderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormularEntryBuilderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormularEntryBuilderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
