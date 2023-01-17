import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficeGridComponent } from './office-grid.component';

describe('OfficeGridComponent', () => {
  let component: OfficeGridComponent;
  let fixture: ComponentFixture<OfficeGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OfficeGridComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfficeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
