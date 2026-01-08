/**
 * @file DataFetcher.vue
 * @description A component that displays a list of construction projects in a data table.
 * Provides functionality to view, create, edit, and delete construction projects.
 * Fetches data from the Symfony API and performs delete operations via the .NET API.
 *
 * @example
 * <DataFetcher />
 */
<template>
    <v-container>
        <v-row>
            <v-col>
                <v-card>
                    <v-card-title>
                        Construction Project List
                    </v-card-title>
                    <v-btn color="primary" @click="createNewItem">Create New Project</v-btn>
                    <v-data-table :headers="headers1" :items="formattedItems" item-key="id" class="elevation-1">
                        <template v-slot:item.actions="{ item }">
                            <v-icon @click="editItem(item.id)" class="mr-2">mdi-pencil</v-icon>
                            <v-icon @click="deleteItem(item.id)">mdi-delete</v-icon>
                        </template>
                    </v-data-table>
                </v-card>
                <v-snackbar v-model="snackbar" :color="errorColor" top multi-line>
                    {{ notifyMessage }}
                </v-snackbar>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>

import { getApiData } from '@/services/symfonyapi';
import { deleteApiData } from '@/services/dotnetapi';

export default {
    /**
     * @data
     * @property {Array} data1 - Array of construction project objects fetched from the API
     * @property {boolean} snackbar - Controls visibility of the notification snackbar
     * @property {string} notifyMessage - Message displayed in the snackbar notification
     * @property {Array} headers1 - Column definitions for the data table including name, stage, category, startDate, and actions
     */
    data() {
        return {
            data1: [],
            snackbar: false,
            notifyMessage: '',
            headers1: [
                { title: 'Name', key: 'name' },
                { title: 'Stage', key: 'stage' },
                { title: 'Category', key: 'category' },
                { title: 'Start Date', key: 'startDate' },
                { title: 'Actions', key: 'actions', sortable: false }
            ]
        };
    },
    computed: {
        /**
         * @computed formattedItems
         * @description Transforms raw API data into display-ready format with formatted dates and stage labels.
         * @returns {Array} Array of construction project objects with formatted startDate and stage properties
         */
        formattedItems() {
            return this.data1.map(item => ({
                ...item,
                startDate: this.formatDate(item.startDate),
                stage: this.formatStage(item.stage)
            }));
        }
    },
    methods: {
        /**
         * Navigates to the create new project page.
         * @returns {void}
         */
        createNewItem() {
            this.$router.push('/create'); // Redirect to create page
        },
        /**
         * Navigates to the edit page for a specific project.
         * @param {number|string} id - The unique identifier of the project to edit
         * @returns {void}
         */
        editItem(id) {
            this.$router.push(`/edit/${id}`); // Redirect to edit page
        },
        /**
         * Formats a date string into a localized date format (DD/MM/YYYY).
         * @param {string|Date} date - The date to format
         * @returns {string} Formatted date string in en-GB locale format
         */
        formatDate(date) {
            return new Intl.DateTimeFormat('en-GB', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit'
            }).format(new Date(date));
        },
        /**
         * Converts a numeric stage value to its human-readable label.
         * @param {number} stage - The stage number (1-4)
         * @returns {string} Human-readable stage name: 'Concept', 'Design & Documentation', 'Pre-Construction', 'Construction', or 'Unknown'
         */
        formatStage(stage) {
            switch (stage) {
                case 1:
                    return 'Concept';
                case 2:
                    return 'Design & Documentation';
                case 3:
                    return 'Pre-Construction';
                case 4:
                    return 'Construction';
                default:
                    return 'Unknown';
            }
        },
        /**
         * Deletes a construction project after user confirmation.
         * Shows a confirmation dialog before deletion and displays success/error notification.
         * @param {number|string} id - The unique identifier of the project to delete
         * @returns {Promise<void>}
         */
        async deleteItem(id) {
            const confirm = window.confirm('Are you sure you want to delete this item?\n\n #' + id);
            if (!confirm) return;

            try {
                deleteApiData(id).then(response => {
                }).catch(error => {
                    console.error("Error fetching data from API:", error);
                });
                this.snackbar = true;
                this.notifyMessage = 'Project #' + id + ' deleted';
                this.fetchItems();
            } catch (error) {
                this.snackbar = true;
                this.notifyMessage = 'Error deleting item:' + error.message;
            }
        },
        /**
         * Fetches all construction projects from the Symfony API.
         * Updates the data1 array with the retrieved projects or shows an error notification on failure.
         * @returns {Promise<void>}
         */
        async fetchItems() {
            try {
                getApiData().then(response => {
                    this.data1 = response.data['hydra:member'];
                }).catch(error => {
                    console.error("Error fetching data from API:", error);
                });
            } catch (error) {
                this.snackbar = true;
                this.notifyMessage = 'Error fetching item:' + error.message;
            }
        },
    },
    /**
     * Lifecycle hook that fetches construction projects when the component is created.
     */
    created() {
        this.fetchItems();
    }
};
</script>
