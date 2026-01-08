<?php

namespace App\Controller;

use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;

/**
 * Controller for application health check endpoints.
 *
 * Provides a simple endpoint to verify that the application is running
 * and responding to requests. Used by load balancers, container orchestrators,
 * and monitoring systems to determine application availability.
 */
class HealthCheckController extends AbstractController
{
    /**
     * Returns the health status of the application.
     *
     * HTTP Method: GET
     * Route: /healthz
     *
     * This endpoint returns a simple JSON response indicating the application
     * is operational. It does not perform deep health checks on dependencies.
     *
     * @return JsonResponse A JSON response with status 'ok' and HTTP 200 status code.
     */
    #[Route('/healthz', name: 'health_check', methods: ['GET'])]
    public function index(): JsonResponse
    {
        return new JsonResponse(['status' => 'ok']);
    }
}
