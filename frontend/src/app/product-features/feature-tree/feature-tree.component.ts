import { Component, Input, OnInit } from '@angular/core';
import { FeatureNode } from '../models/feature-node.model';

@Component({
    selector: 'app-feature-tree',
    templateUrl: './feature-tree.component.html',
    styleUrls: ['./feature-tree.component.scss']
})
export class FeatureTreeComponent implements OnInit {
    @Input() features: FeatureNode[] = [];
    @Input() expandedNodes: Set<string> = new Set();

    constructor() { }

    ngOnInit(): void { }

    toggleNode(nodeId: string): void {
        if (this.expandedNodes.has(nodeId)) {
            this.expandedNodes.delete(nodeId);
        } else {
            this.expandedNodes.add(nodeId);
        }
    }

    isExpanded(nodeId: string): boolean {
        return this.expandedNodes.has(nodeId);
    }

    hasChildren(node: FeatureNode): boolean {
        return node.children && node.children.length > 0;
    }
} 