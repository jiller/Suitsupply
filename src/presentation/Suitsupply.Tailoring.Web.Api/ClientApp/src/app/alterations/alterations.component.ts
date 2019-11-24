import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Alteration } from "../common/alteration";
import {Router} from "@angular/router";

@Component({
  selector: 'app-alterations',
  templateUrl: './alterations.component.html'
})
export class AlterationsComponent {
  public alterations: Alteration[];

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string,
              private router: Router) {
    http.get<Alteration[]>(baseUrl + '/alterations').subscribe((result: any) => {
      this.alterations = result.alterations;
    }, error => console.error(error));
  }

  viewAlteration(alteration: Alteration) {
    this.router.navigateByUrl('alterations/view', {state: {data: alteration}});
  }
}
