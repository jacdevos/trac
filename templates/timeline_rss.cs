<?xml version="1.0"?>
<rss version="2.0">
   <?cs set base_url = "http://"+$HTTP.Host ?>
   <?cs def:rss_item(category,title, link, descr) ?>
      <item>
        <author><?cs var:$item.author ?></author>
        <pubDate><?cs var:$item.datetime ?></pubDate>
        <title><?cs var:$title ?></title>	  
        <link><?cs var:$base_url ?><?cs var:$link ?></link>
        <description><?cs var:$descr ?></description>
        <category><?cs var:$category ?></category>
      </item>
   <?cs /def ?>
    <channel>
      <?cs if $project.name ?>
        <title><?cs var:$project.name?>: <?cs var:$title ?></title>
      <?cs else ?>
        <title><?cs var:$title ?></title>
      <?cs /if ?>
      <link><?cs var:$base_url ?><?cs var:$trac.href.timeline ?></link>
      <description>Trac Timeline</description>
      <language>en-us</language>
      <generator>Trac <?cs var:$trac.version ?></generator>
      <?cs each:item = $timeline.items ?>
        <?cs if:item.type == #1 ?><!-- Changeset -->
          <?cs call:rss_item('Changeset',
                             '['+$item.data+']: '+$item.shortmsg, 
                             $item.changeset_href, $item.message) ?>
        <?cs elif:item.type == #2 ?><!-- New ticket -->
          <?cs call:rss_item('Ticket',
                             '#'+$item.data+' created: '+$item.shortmsg,
                             $item.ticket_href, $item.message) ?>
        <?cs elif:item.type == #3 ?><!-- Closed ticket -->
          <?cs call:rss_item('Ticket',
                             '#'+$item.data+' resolved: '+$item.shortmsg,
                             $item.ticket_href, $item.message) ?>
        <?cs elif:item.type == #4 ?><!-- Reopened ticket -->
          <?cs call:rss_item('Ticket',
                             '#'+$item.data+' reopened: '+$item.shortmsg,
                             $item.ticket_href, $item.message) ?>
        <?cs elif:item.type == #5 ?><!-- Reopened ticket -->
          <?cs call:rss_item('Wiki',
                             $item.data+" page edited.",
                             $item.wiki_href,
'Wiki page <a href="'+$base_url+$item.wiki_href+'">'+$item.data+'</a> edited by '+$item.author) ?>
        <?cs /if ?>
      <?cs /each ?>
    </channel>
</rss>
