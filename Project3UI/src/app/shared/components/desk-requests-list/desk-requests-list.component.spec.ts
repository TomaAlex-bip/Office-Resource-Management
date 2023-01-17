import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeskRequestsListComponent } from './desk-requests-list.component';

describe('DeskRequestsListComponent', () => {
  let component: DeskRequestsListComponent;
  let fixture: ComponentFixture<DeskRequestsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeskRequestsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeskRequestsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
