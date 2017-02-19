import { Injectable }    from '@angular/core';
import { Headers, Http, Response, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import 'rxjs/add/operator/toPromise';

import { Task } from '../_models/task';
import { User } from '../_models/user';
import { Status } from '../_models/status';
import { TaskType } from '../_models/tasktype';
import {TreeNode} from 'primeng/primeng';

@Injectable()
export class TaskService {
  private headers = new Headers({'Content-Type': 'application/json'});
  // private projectsUrl = 'http://172.16.106.158:60362/api/projectsapi/getprojects';  // URL to web api
  private tasksUrl = 'http://localhost:60362/api/tasksapi';  // URL to web api
  private taskDetailsUrl = 'http://localhost:60362/api/taskdetailsapi';
  //private projectsUrl = 'https://jsonblob.com/api/jsonblob/80081f0b-e88c-11e6-90ab-53b8e59ea410';  // URL to web api

  constructor(private http: Http) { }

  getTasks(projectId: number): Observable<TreeNode[]> {
    return this.http.get(this.tasksUrl + "/gettasks/" + projectId)
                  .map(res => <TreeNode[]> res.json())
                  .catch(this.handleError);
  }

  getTaskDetails(taskId: number): Observable<Task> {
    return this.http.get(this.taskDetailsUrl + "/gettask/" + taskId)
                  .map(this.extractData)
                  .catch(this.handleError);
  }

  getUsers(): Observable<User[]> {
    return this.http.get(this.taskDetailsUrl + "/getusers")
                  .map(this.extractData)
                  .catch(this.handleError);
  }

  getStatus(): Observable<Status[]> {
    return this.http.get(this.taskDetailsUrl + "/getstatus")
                  .map(this.extractData)
                  .catch(this.handleError);
  }

  getTaskTypes(): Observable<TaskType[]> {
    return this.http.get(this.taskDetailsUrl + "/gettasktypes")
                  .map(this.extractData)
                  .catch(this.handleError);
  }

  addTask(task: Task) {
    var req = JSON.stringify(task, ['Id', 'Title', 'StartedOn', 'EndedOn', 'Url',
                                    'StatusId', 'TaskTypeId', 'AssignedUserId',
                                    'EstimatedEndsOn', 'UserId', 'ParentTaskId',
                                    'ProjectId', 'Description', 'CreatedOn', 
                                    'CompletedPercent']);
    console.log("req " + req);
    
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    
    this.http.post(this.taskDetailsUrl + "/addtask", req, options)
                  .map((res: Response) => res).subscribe(res => {});
  }
  editTask(task: Task) {
    var req = JSON.stringify(task, ['Id', 'Title', 'StartedOn', 'EndedOn', 'Url',
                                    'StatusId', 'TaskTypeId', 'AssingedUserId',
                                    'EstimatedEndsOn', 'UserId', 'ParentTaskId',
                                    'ProjectId', 'Description', 'CreatedOn', 
                                    'CompletedPercent']);
    console.log("req "+req);
    
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    
    this.http.put(this.taskDetailsUrl + "/edittask/" + task.Id, req, options)
                  .map((res: Response) => res.json()).subscribe(res => {});
  }
  
  deleteTask(taskId: number) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    
    this.http.delete(this.tasksUrl + "/deletetask/" + taskId, options)
                  .map((res: Response) => res.json()).subscribe(res => {});
  }
  
  private extractData(res: Response) {
    let body = res.json();
    console.log("something got" + JSON.stringify(body));
    return body;
  }
  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }

}
