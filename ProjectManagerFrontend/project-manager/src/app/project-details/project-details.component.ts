import { Component, inject, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { ProjectsService } from '../projects/projects.service';
import { CommonModule } from '@angular/common';  

@Component({
  selector: 'app-project-details',
  imports: [
    CommonModule,
    MatTableModule,
    MatSortModule],
  templateUrl: './project-details.component.html',
  styleUrl: './project-details.component.css'
})

export class ProjectDetailsComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private projectsService = inject(ProjectsService);

   projectDetails = this.projectsService.loadedProjectDetails;

  displayedColumns = ['name', 'assigneeName', 'status'];
  
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      console.error("Id is missing!");
      return;
    }
    const sub = this.projectsService.loadProjectDetails(id)
      .subscribe({
        error: (error: Error) => {
          console.log(error);
        },
        complete: () => {
          console.log(this.projectDetails());
        },
      });
  }
  
  


}
