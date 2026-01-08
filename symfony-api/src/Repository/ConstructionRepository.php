<?php

namespace App\Repository;

use App\Entity\Construction;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

/**
 * Repository for managing Construction entities.
 *
 * Provides data access methods for the Construction entity.
 * Extends ServiceEntityRepository for standard CRUD operations including:
 * - find($id) - Find a construction by its primary key
 * - findOneBy(array $criteria) - Find a single construction by criteria
 * - findAll() - Retrieve all constructions
 * - findBy(array $criteria, array $orderBy, $limit, $offset) - Find constructions by criteria
 *
 * @extends ServiceEntityRepository<Construction>
 * @see Construction
 */
class ConstructionRepository extends ServiceEntityRepository
{
    /**
     * Initializes the repository with the entity manager registry.
     *
     * @param ManagerRegistry $registry The Doctrine manager registry.
     */
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Construction::class);
    }
}
