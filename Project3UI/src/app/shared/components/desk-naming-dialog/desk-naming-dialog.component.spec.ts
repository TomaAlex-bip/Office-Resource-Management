import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeskNamingDialogComponent } from './desk-naming-dialog.component';

describe('DeskNamingDialogComponent', () => {
  let component: DeskNamingDialogComponent;
  let fixture: ComponentFixture<DeskNamingDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeskNamingDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeskNamingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
