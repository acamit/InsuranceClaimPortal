import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css']
})
export class supportComponent {
  private baseUrl: string ="https://localhost:7189/api/Email"
  emailForm!:FormGroup;
  constructor(private http: HttpClient, private fb:FormBuilder,private router: Router) { }

  ngOnInit(){
    this.emailForm=this.fb.group({
      to:['',Validators.required],
      subject:['', Validators.required],
      body:['', Validators.required]
    })
  }
  email: string ='abc@gmail.com';
  sendEmail() {
    // alert("fuction run")

    console.log(this.emailForm.value)
    if(this.emailForm.valid){
      //send object to database
      console.log(this.emailForm.value)
      this.http.post(this.baseUrl, this.emailForm.value)
      .subscribe({
        next:(res)=>{

          alert('Email sent successfully!');
        },
        error:(err)=>{

          this.emailForm.reset();
        }
      })
    }
    else{
      //throw error
      console.log("invalid")

      alert("your form is invalid")
    }


  }
}
