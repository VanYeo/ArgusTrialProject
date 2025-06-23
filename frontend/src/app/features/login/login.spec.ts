import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { LoginService } from './login.service';

describe('LoginComponent (with inject())', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockLoginService: jasmine.SpyObj<LoginService>;

  beforeEach(async () => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    mockLoginService = jasmine.createSpyObj('LoginService', ['submitLoginData']);

    await TestBed.configureTestingModule({
      declarations: [LoginComponent],
      imports: [FormsModule],
      providers: [
        { provide: Router, useValue: mockRouter },
        { provide: LoginService, useValue: mockLoginService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the login component', () => {
    expect(component).toBeTruthy();
  });

  it('should toggle password visibility', () => {
    expect(component.showPassword).toBeFalse();
    component.togglePassword();
    expect(component.showPassword).toBeTrue();
  });

  it('should disable submit and set error message if fields are empty', () => {
    component.loginData = { email: '', password: '' };
    component.assignErrorMessage();
    expect(component.disableSubmit).toBeTrue();
    expect(component.errorMessage).toBe('Email and password fields are required');
  });

  it('should call LoginService and navigate to dashboard on successful login', () => {
    component.loginData = { email: 'user@example.com', password: '123456' };
    mockLoginService.submitLoginData.and.returnValue(of('fake-token'));

    component.onSubmit();

    expect(mockLoginService.submitLoginData).toHaveBeenCalledWith(component.loginData);
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/dashboard']);
    expect(component.errorMessage).toBe('');
  });

  it('should handle login failure and display error', () => {
    const error = { error: 'Invalid login' };
    component.loginData = { email: 'bad@user.com', password: 'wrong' };
    mockLoginService.submitLoginData.and.returnValue(throwError(() => error));

    component.onSubmit();

    expect(component.errorMessage).toBe('Invalid login');
    expect(mockRouter.navigate).not.toHaveBeenCalled();
  });
});
