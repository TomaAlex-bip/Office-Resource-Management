import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeskReserveDialogComponent } from './desk-reserve-dialog.component';

describe('DeskReserveDialogComponent', () => {
  let component: DeskReserveDialogComponent;
  let fixture: ComponentFixture<DeskReserveDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeskReserveDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeskReserveDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
