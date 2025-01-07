import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionExportComponent } from './transaction-export.component';

describe('TransactionExportComponent', () => {
  let component: TransactionExportComponent;
  let fixture: ComponentFixture<TransactionExportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransactionExportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransactionExportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
