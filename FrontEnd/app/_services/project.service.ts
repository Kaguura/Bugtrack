import { Injectable }    from '@angular/core';
import { Headers, Http, Response, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import 'rxjs/add/operator/toPromise';

import { Project } from '../_models/project';

@Injectable()
export class ProjectService {
  private headers = new Headers({'Content-Type': 'application/json'});
  // private projectsUrl = 'http://172.16.106.158:60362/api/projectsapi/getprojects';  // URL to web api
  private projectsUrl = 'http://localhost:60362/api/projectsapi';  // URL to web api
  //private projectsUrl = 'https://jsonblob.com/api/jsonblob/80081f0b-e88c-11e6-90ab-53b8e59ea410';  // URL to web api

  constructor(private http: Http) { }

  /*getProjects(): Promise<Project[]> {
    return this.http.get(this.projectsUrl)
               .toPromise()
               .then(response => response.json().data as Project[])
               .catch(this.handleError);
  }*/
  getProjects(token: string): Observable<Project[]> {
    console.log('Bearer ' +  token);
    let headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append('Authorization', 'Bearer ' + token);
    let options = new RequestOptions({ headers: headers });
    return this.http.get(this.projectsUrl + "/getprojects", options)
                  .map(this.extractData)
                  .catch(this.handleError);
  }
  addProject(project: Project) {
    var req = JSON.stringify(project, ['Name', 'ParentId']);
    
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    
    this.http.post(this.projectsUrl + "/addproject", req, options)
                  .map((res: Response) => res.json()).subscribe(res => {});
  }
  editProject(project: Project) {
    var req = JSON.stringify(project, ['Id', 'Name', 'ParentId']);
    console.log("req "+req);
    
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    
    this.http.put(this.projectsUrl + "/editproject/" + project.Id, req, options)
                  .map((res: Response) => res.json()).subscribe(res => {});
  }

  deleteProject(project: Project) {    
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    
    this.http.delete(this.projectsUrl + "/deleteproject/" + project.Id, options)
                  .map((res: Response) => res.json()).subscribe(res => {});
  }
  
  private extractData(res: Response) {
    let body = res.json();
    console.log("something got" + JSON.stringify(body));
    return body;
  }
  /*getProject(id: number): Promise<Project> {
    const url = `${this.projectsUrl}/${id}`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json().data as Project)
      .catch(this.handleError);
  }*/
  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }

}
