import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { TaskDialogComponent, TaskDialogData } from './task-dialog.component';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
describe('TaskDialogComponent', () => {
  let component: TaskDialogComponent;
  let fixture: ComponentFixture<TaskDialogComponent>;

  const dialogDataMock: TaskDialogData = {
    projectId: 'p1',
    task: {
      id: 't1',
      name: 'Test task',
      status: 'New',
    } as any,
  };


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskDialogComponent],
       providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: MAT_DIALOG_DATA, useValue: dialogDataMock },
        { provide: MatDialogRef, useValue: {} },
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(TaskDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
