FROM logstash

COPY logstash.conf /bin/
RUN mkdir logs
RUN mkdir logs/app
RUN mkdir logs/web

RUN logstash-plugin install logstash-filter-grok

CMD ["-f", "/bin/logstash.conf"]