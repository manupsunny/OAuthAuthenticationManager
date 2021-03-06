﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Utilities.Models;
using Authentication.Utilities.Repositories;

namespace Authentication.Utilities.Services
{
    public class ConsumerKeyService : IConsumerKeyService
    {
        private readonly IConsumerKeyRepository ConsumerKeyRepository;

        public ConsumerKeyService(IConsumerKeyRepository consumerKeyRepository)
        {
            ConsumerKeyRepository = consumerKeyRepository;
        }

        public IEnumerable<ConsumerKey> GetAllConsumerKeysSync()
        {
            return GetAllConsumerKeysAsync().Result.ToList();
        }

        public async Task<IEnumerable<ConsumerKey>> GetAllConsumerKeysAsync()
        {
            return await ConsumerKeyRepository.FindAll().ConfigureAwait(false);
        }

        public async Task<string> GetConsumerChannel(string consumerKey)
        {
            var consumerChannel = await ConsumerKeyRepository.Find(consumerKey);
            return consumerChannel.Channel;
        }
    }
}