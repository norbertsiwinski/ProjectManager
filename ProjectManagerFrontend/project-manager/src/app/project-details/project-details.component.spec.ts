import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProjectDetailsComponent } from './project-details.component';
import { provideHttpClient } from '@angular/common/http';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ProjectsService } from '../projects/projects.service';
import { signal } from '@angular/core';
import { of } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';

describe('ProjectDetailsComponent', () => {
 const TEST_PROJECT_ID = 123; 

  let component: ProjectDetailsComponent;
  let fixture: ComponentFixture<ProjectDetailsComponent>;
  let service: ProjectsService;
  let dialog: MatDialog;

  let projectsServiceMock: any;
  let matDialogMock: any;

  beforeEach(async () => {

    const projectDetailsSig = signal<any>(null);
    const projectMembersSig = signal<any[]>([]);

    matDialogMock = {
      open: jasmine.createSpy('open').and.returnValue({
        afterClosed: () => of(true)
      })
    }

    projectsServiceMock = {
      loadedProjectDetails: projectDetailsSig.asReadonly(),
      loadedProjectMembers: projectMembersSig.asReadonly(),

      addTask: jasmine.createSpy('addTask').and.returnValue(of({})),
      loadProjectDetails: jasmine.createSpy('loadProjectDetails').and.returnValue(of({})),
      loadUserProjectDetails: jasmine.createSpy('loadUserProjectDetails').and.returnValue(of({})),
    };

    await TestBed.configureTestingModule({
      imports: [ProjectDetailsComponent],
      providers: [provideHttpClient(),
      provideHttpClientTesting(),
      {
        provide: ActivatedRoute,
        useValue: { snapshot: { paramMap: convertToParamMap({ id: TEST_PROJECT_ID }) } }
      },
      {
        provide: ProjectsService,
        useValue: projectsServiceMock
      },
      {
        provide: MatDialog,
        useValue: matDialogMock
      }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ProjectDetailsComponent);
    service = TestBed.inject(ProjectsService);
    dialog = TestBed.inject(MatDialog);

    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call load project details after adding task', () => {
    const projectId = '1';
    const assigneeId = '2';
    const taskName = 'testTask';
    component.form.controls.taskName.setValue(taskName);
    component.form.controls.assigneeId.setValue(assigneeId);

    component.addTask(projectId);

    expect(projectsServiceMock.addTask).toHaveBeenCalledWith(projectId, taskName, assigneeId);
    expect(projectsServiceMock.loadProjectDetails).toHaveBeenCalledWith(projectId);
  })

  it('should open task dialog and pass data', () => {

    const task = {};
    component.openTaskDialog(task);

    expect(dialog.open).toHaveBeenCalledWith(TaskDialogComponent, jasmine.objectContaining({
      data: {task: task, projectId: TEST_PROJECT_ID}
    }));

  })

});
