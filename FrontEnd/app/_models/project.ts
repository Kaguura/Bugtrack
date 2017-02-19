export class Project{
  Id: number;
  Name: string;
  children: Project[];
  ParentId: number;
  ParentName: string;
}
