import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { ProjectsService } from './projects.service';

describe('ProjectsService', () => {
  let service: ProjectsService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
      ],
    });
    service = TestBed.inject(ProjectsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should add task', () => {
    const id = '1';
    const name = 'task1';
    const projectMemberId = '2';

    service.addTask(id, name, projectMemberId).subscribe();

    const req = httpMock.expectOne(`https://localhost:7249/api/projects/${id}/taskItems`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ name, projectMemberId })

    req.flush({});
  })

});
