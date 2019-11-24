import {Pipe, PipeTransform} from "@angular/core";
import {Shortening} from "../common/shortening";

@Pipe({name: 'shortening'})
export class ShorteningPipe implements PipeTransform {
  transform(value: Shortening): string {
    if (value) {
      return "[ left: " + value.left + "; right: " + value.right + "]";
    }
    return "";
  }
}
