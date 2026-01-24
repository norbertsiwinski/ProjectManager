import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { UsersComponent } from './users.component';
import { UsersService } from './users.service';
import { signal } from '@angular/core';
import { UniqueSelectionDispatcher } from '@angular/cdk/collections';
import { of } from 'rxjs';
import { User } from './user.model';
import { By } from '@angular/platform-browser';

describe('UsersComponent', () => {
  let component: UsersComponent;
  let fixture: ComponentFixture<UsersComponent>;

  const users: User[] = [{ id: '1', email: 'john@gmail.com', role: 'Admin' }]
  const usersSig = signal(users);

  const userServiceMock = {
    loadedUsers: usersSig.asReadonly(),
    getAllUsers: jasmine.createSpy().and.returnValue(of([]))
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsersComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        { provide: UsersService, useValue: userServiceMock }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(UsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should expose users from signal', () => {
    fixture.detectChanges();
    expect(component.users()).toEqual(users);
  });

  it('should show user column', () => {
    fixture.detectChanges();
    const headerElements = fixture.debugElement.queryAll(By.css('th[mat-header-cell]'));
    const headers = headerElements
      .map(h => (h.nativeElement.textContent as string).trim());

    expect(headers).toEqual(['Name', 'Role']);
  });
});
