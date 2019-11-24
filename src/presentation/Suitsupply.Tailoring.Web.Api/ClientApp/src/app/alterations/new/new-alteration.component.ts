import { Component, OnInit, Inject } from '@angular/core'
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";

@Component({
  selector: 'app-alterations',
  templateUrl: './new-alteration.component.html'
})
export class NewAlterationsComponent implements OnInit {
  public error: any;
  public alterationForm: FormGroup;

  constructor(private fb: FormBuilder,
              private http: HttpClient,
              @Inject('BASE_API_URL') private baseUrl: string,
              private router: Router){
    this.ngOnInit();
  }

  ngOnInit() {
    this.alterationForm = new FormGroup({
      shortenSleeves: new FormGroup({
        left: new FormControl(0, [Validators.required, Validators.max(5), Validators.min(-5)]),
        right: new FormControl(0, [Validators.required, Validators.max(5), Validators.min(-5)])
      }),
      shortenTrousers: new FormGroup({
        left: new FormControl(0, [Validators.required, Validators.max(5), Validators.min(-5)]),
        right: new FormControl(0, [Validators.required, Validators.max(5), Validators.min(-5)])
      }),
      customerId: new FormControl('', [Validators.required, Validators.min(0)])
    });
  }

  onSubmit(){
    console.warn(this.alterationForm.value);

    this.http
      .post(this.baseUrl + '/alterations', this.alterationForm.value)
      .subscribe((result: any) => {
        if (result.isSuccess){
          return this.router.navigateByUrl("/alterations");
        }
      }, error => {
        console.error(error)
        this.error = error;
      });
  }
}
