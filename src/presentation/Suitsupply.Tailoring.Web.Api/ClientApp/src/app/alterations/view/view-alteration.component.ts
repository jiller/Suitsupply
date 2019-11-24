import {Component, Inject, Input} from '@angular/core'
import { HttpClient } from '@angular/common/http';
import {ActivatedRoute, Router} from "@angular/router";
import {Alteration} from "../../common/alteration";

@Component({
  selector: 'app-alterations',
  templateUrl: './view-alteration.component.html'
})
export class ViewAlterationsComponent {
  public error: any;
  public alteration: Alteration;
  public id: number;

  constructor(private currentRoute: ActivatedRoute,
              private http: HttpClient,
              @Inject('BASE_API_URL') private baseUrl: string,
              private router: Router){
    let params : any = this.currentRoute.params;
    this.id = params.value.id;

    this.http.get(this.baseUrl + '/alterations/' + this.id)
      .subscribe((result: Alteration) => {
        this.alteration = result
      }, error => {
        console.error(error);
        this.error = error;
      });
  }

  markAlterationAsPaid() {
    this.http.post(this.baseUrl + '/alterations/' + this.id + '/paid', {})
      .subscribe(result => {
        return this.router.navigateByUrl('alterations');
      }, error => {
        console.error(error);
        this.error = error;
      });
  }

  markAlterationAsDone() {
    this.http.post(this.baseUrl + '/alterations/' + this.id + '/done', {})
      .subscribe(result => {
        return this.router.navigateByUrl('alterations');
      }, error => {
        console.error(error);
        this.error = error;
      });
  }
}
