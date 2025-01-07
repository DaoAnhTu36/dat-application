import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseDeleteComponent } from './warehouse-delete.component';

describe('WarehouseDeleteComponent', () => {
  let component: WarehouseDeleteComponent;
  let fixture: ComponentFixture<WarehouseDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseDeleteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WarehouseDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
