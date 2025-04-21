import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-feature-list',
    standalone: true,
    imports: [],
    templateUrl: './feature-list.component.html',
    styleUrls: ['./feature-list.component.css']
})
export class FeatureListComponent implements OnInit {
    features: any[] = [];

    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        this.loadFeatures();
    }

    loadFeatures(): void {
        // Replace with real API later
        this.features = [
            { name: 'Login', status: 'Enabled', description: 'User authentication' },
            { name: 'Test Cases', status: 'Enabled', description: 'Manage test cases' },
            { name: 'Reports', status: 'Coming Soon', description: 'Generate release reports' }
        ];
    }
}
