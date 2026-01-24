import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProjectDetailsComponent } from './project-details.component';
import { provideHttpClient } from '@angular/common/http';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ProjectsService } from '../projects/projects.service';
import { signal } from '@angular/core';
import { of } from 'rxjs';

describe('ProjectDetailsComponent', () => {
  let component: ProjectDetailsComponent;
  let fixture: ComponentFixture<ProjectDetailsComponent>;
  let service: ProjectsService;

  let projectsServiceMock: any;

  beforeEach(async () => {

    const projectDetailsSig = signal<any>(null);
    const projectMembersSig = signal<any[]>([]);

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
        useValue: { snapshot: { paramMap: convertToParamMap({ id: 123 }) } }
      },
      {
        provide: ProjectsService,
        useValue: projectsServiceMock
      },
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ProjectDetailsComponent);
    service = TestBed.inject(ProjectsService);

    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call load project details after adding task', () => {
    const projectId = '1';
    component.form.controls.taskName.setValue('testTask');
    component.form.controls.assigneeId.setValue('1');

    component.addTask(projectId);

    expect(projectsServiceMock.addTask).toHaveBeenCalledWith('1', 'testTask', '1');
    expect(projectsServiceMock.loadProjectDetails).toHaveBeenCalledWith('1');
  })

});
