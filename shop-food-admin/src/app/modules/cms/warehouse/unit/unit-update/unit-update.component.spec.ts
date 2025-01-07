import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitUpdateComponent } from './unit-update.component';

describe('UnitUpdateComponent', () => {
  let component: UnitUpdateComponent;
  let fixture: ComponentFixture<UnitUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UnitUpdateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnitUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
