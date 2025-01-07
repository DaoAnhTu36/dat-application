import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionDetailSubComponent } from './transaction-detail-sub.component';

describe('TransactionDetailSubComponent', () => {
  let component: TransactionDetailSubComponent;
  let fixture: ComponentFixture<TransactionDetailSubComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransactionDetailSubComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransactionDetailSubComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
