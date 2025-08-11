import { Component, inject, OnInit, output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe, NgIf } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { DatePickerComponent } from '../_forms/date-picker/date-picker.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe, NgIf, TextInputComponent, DatePickerComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {

  private accountService = inject(AccountService)
  private fb = inject(FormBuilder)
  private toastr = inject(ToastrService)

  cancelRegister = output<boolean>()
  model: any = {}
  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date()

  ngOnInit(): void {
    this.initializeForm()
    //mayor a 18 años
    this.maxDate.setFullYear(this.maxDate.getFullYear()- 18)
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
    //esto para actualizar el custom validator si es que el valor base cambia
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  //custom validator para el confirm password
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      //si retornamos true, significa q no hay match
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    }
  }

  register() {
    console.log(this.registerForm.value)
    /*
    this.accountService.register(this.model).subscribe({
      next: response =>{
        console.log(response)
        this.cancel()
      },
      error : error=> this.toastr.error(error.error)
    })*/
  }
  cancel() {
    this.cancelRegister.emit(false)
  }

}
