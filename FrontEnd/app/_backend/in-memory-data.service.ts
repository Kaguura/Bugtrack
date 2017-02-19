import { InMemoryDbService } from 'angular-in-memory-web-api';
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    let projects = [
    {
      Id: 1,
      Name: 'bugtrack',
      children: [
        { Id: 2, Name: 'backend', tasks: [{Id: 11, title: "rewrite!"},{Id: 12, title: "do super backend!"}]},
        { Id: 3, Name: 'frontend', tasks: [{Id: 14, title: "relax!"}]}
      ],
      tasks: [{Id: 31, title: "task#1"},{Id: 32, title: "task#2"},{Id: 33, title: "task#3"}]
    },
    {
      Id: 4,
      Name: 'google',
      children: [
        { Id: 5, Name: 'google glass' },
        {
          Id: 6,
          Name: 'translator',
          children: [
            { Id: 7, Name: 'kz' }
          ]
        }
      ],
      tasks: [{Id: 31, title: "task#1"},{Id: 32, title: "task#2"},{Id: 33, title: "task#3"}]
    }];
    return {projects};
  }
}
