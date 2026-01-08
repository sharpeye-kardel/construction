/**
 * @file Login.vue
 * @description View component for user authentication.
 *              Provides a login form with email and password fields,
 *              handles authentication via Vuex store action, and displays
 *              error messages using a snackbar notification.
 * @route /login - Route for user authentication
 */
<template>
    <v-container>
        <v-row justify="center">
            <v-col cols="12" md="4">
                <v-card>
                    <v-card-title>Login</v-card-title>
                    <v-card-text>
                        <v-form @submit.prevent="login">
                            <v-text-field v-model="email" label="Email" required></v-text-field>
                            <v-text-field v-model="password" label="Password" type="password" required></v-text-field>
                            <v-btn type="submit" color="primary">Login</v-btn>
                        </v-form>
                    </v-card-text>
                </v-card>
                <v-snackbar v-model="snackbar" :color="errorColor" top multi-line>
                    {{ errorMessage }}
                    <v-btn color="white" text @click="snackbar = false">Close</v-btn>
                </v-snackbar>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import { mapActions } from 'vuex';

export default {
    /**
     * @data
     * @property {string} email - User's email address input, initially empty
     * @property {string} password - User's password input, initially empty
     * @property {boolean} snackbar - Controls visibility of the error snackbar notification
     * @property {string} errorMessage - Error message to display in the snackbar
     * @property {Array<Function>} emailRules - Validation rules for the email field
     * @property {Array<Function>} passwordRules - Validation rules for the password field
     * @returns {Object} The initial data state for the component
     */
    data() {
        return {
            email: '',
            password: '',
            snackbar: false,
            errorMessage: '',
            emailRules: [
                v => !!v || 'Email is required',
                v => /.+@.+\..+/.test(v) || 'E-mail must be valid'
            ],
            passwordRules: [
                v => !!v || 'Password is required',
                v => (v && v.length >= 6) || 'Password must be at least 6 characters'
            ]
        };
    },
    methods: {
        ...mapActions({ loginAction: 'loginTo' }),
        /**
         * Handles user login by dispatching the login action to the Vuex store.
         * On successful authentication, navigates the user to the home page.
         * On failure, displays an error message in a snackbar notification.
         *
         * @returns {Promise<void>} Resolves when login completes (success or failure handled)
         */
        async login() {
            try {
                await this.loginAction({ email: this.email, password: this.password });
                this.$router.push('/');
            } catch (error) {
                this.snackbar = true;
                this.errorMessage = error.message || 'Login failed. Please try again.';
            }
        }
    }
};
</script>