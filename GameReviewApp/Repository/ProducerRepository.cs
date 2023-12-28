﻿using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        public readonly DataContext _context;
        public ProducerRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Game> GetGameByProducer(int producerid)
        {
            return _context.GamesProducers.Where(g => g.Producer.Id == producerid).Select(g => g.Game).ToList();
        }

        public Producer GetProducerById(int producerid)
        {
            return _context.Producers.Where(g => g.Id == producerid).FirstOrDefault();
        }

        public ICollection<Producer> GetProducerOfAGames(int gameId)
        {
            return _context.GamesProducers.Where(g => g.Game.Id == gameId).Select(p => p.Producer).ToList();
        }

        public ICollection<Producer> GetProducers()
        {
            return _context.Producers.OrderBy(p => p.Id).ToList();
        }

        public bool ProducerExists(int producerid)
        {
            return _context.Producers.Any(p => p.Id == producerid);
        }
    }
}
