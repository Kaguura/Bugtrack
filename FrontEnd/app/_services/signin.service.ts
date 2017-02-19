import { Injectable }    from '@angular/core';
import { Headers, Http, Response, RequestOptions } from '@angular/http';
import { Token } from '../_models/token';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import 'rxjs/add/operator/toPromise';

import { Project } from '../_models/project';

@Injectable()
export class SigninService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private signinUrl = 'http://localhost:60362/token';  // URL to web api

  constructor(private http: Http) { }

  signin(username: string, password: string): Observable<Token> {
    return Observable.fromPromise(new Promise((resolve, reject) => {
      let formData: FormData = new FormData();
      formData.append('username', username);
      formData.append('password', password);

      let xhr: XMLHttpRequest = new XMLHttpRequest();

      //xhr.setRequestHeader("enctype", "multipart/form-data");
      /*xhr.setRequestHeader("Cache-Control", "no-cache");
      xhr.setRequestHeader("Cache-Control", "no-store");
      xhr.setRequestHeader("Pragma", "no-cache");
  */
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
          if (xhr.status === 200) {
            //console.log(xhr.response);
            resolve(JSON.parse(xhr.response));
          }
          else {
            reject(xhr.status)
          }
        }
      };

      xhr.open('POST', this.signinUrl, true);
      xhr.send(formData);
    }));

    //console.log(JSON.stringify(xhr.response));

/*
    let arr = [username, password];
    var req = JSON.stringify(arr);

    let headers = new Headers({ 'Content-Type': 'multipart/form-data' });
    let options = new RequestOptions({ headers: headers });   

    this.http.post(this.signinUrl, req, options).map((res: Response) => res.json()).subscribe(res => {this.token = res});*/
    //return this.token;
  }

}
