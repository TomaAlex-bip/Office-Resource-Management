import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeskDeleteDialogComponent } from './desk-delete-dialog.component';

describe('DeskDeleteDialogComponent', () => {
  let component: DeskDeleteDialogComponent;
  let fixture: ComponentFixture<DeskDeleteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeskDeleteDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeskDeleteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
