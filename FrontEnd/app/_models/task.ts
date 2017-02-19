export class Task{
  Id: number;
  Title: string;
  StartedOn: Date;
  EndedOn: Date;
  Url: string;
  StatusId: number;
  TaskTypeId: number;
  AssignedUserId: string;
  EstimatedEndsOn: Date;
  UserId: string;
  ParentTaskId: number;
  ProjectId: number;
  Description: string;
  CreatedOn: Date;
  CompletedPercent: number;
  private _children: Task[] = [];
  
  get children() {
    return this._children;
  }
  set children(ch: Task[]) {
    this._children = ch;
  }
}
